#!/usr/bin/env python3
"""
Simple background generator for Blue Archive character themes
Creates gradient backgrounds with visual elements using pure Python
"""

import os
import struct
import zlib

def create_png(width, height, pixel_data):
    """Create a PNG file from raw pixel data"""
    
    def png_chunk(chunk_type, data):
        chunk = chunk_type + data
        crc = zlib.crc32(chunk) & 0xffffffff
        return struct.pack('>I', len(data)) + chunk + struct.pack('>I', crc)
    
    # PNG signature
    signature = b'\x89PNG\r\n\x1a\n'
    
    # IHDR chunk
    ihdr_data = struct.pack('>IIBBBBB', width, height, 8, 2, 0, 0, 0)
    ihdr = png_chunk(b'IHDR', ihdr_data)
    
    # IDAT chunk (image data)
    raw_data = b''
    for y in range(height):
        raw_data += b'\x00'  # Filter type: None
        for x in range(width):
            idx = y * width + x
            r, g, b = pixel_data[idx]
            raw_data += bytes([r, g, b])
    
    compressed = zlib.compress(raw_data)
    idat = png_chunk(b'IDAT', compressed)
    
    # IEND chunk
    iend = png_chunk(b'IEND', b'')
    
    return signature + ihdr + idat + iend

def interpolate_color(c1, c2, t):
    """Interpolate between two RGB colors"""
    r = int(c1[0] + (c2[0] - c1[0]) * t)
    g = int(c1[1] + (c2[1] - c1[1]) * t)
    b = int(c1[2] + (c2[2] - c1[2]) * t)
    return (r, g, b)

def hex_to_rgb(hex_color):
    """Convert hex color to RGB tuple"""
    return tuple(int(hex_color[i:i+2], 16) for i in (0, 2, 4))

def generate_hina_gradient(width, height, theme_name):
    """Generate Hina-style gradient background"""
    colors = [
        hex_to_rgb('#3E1249'),  # Top
        hex_to_rgb('#2A0D33'),  # Mid
        hex_to_rgb('#17061D'),  # Bottom
    ]
    
    pixels = []
    half_height = height // 2
    
    for y in range(height):
        if y < half_height:
            t = y / half_height
            r, g, b = interpolate_color(colors[0], colors[1], t)
        else:
            t = (y - half_height) / half_height
            r, g, b = interpolate_color(colors[1], colors[2], t)
        
        pixels.extend([(r, g, b)] * width)
    
    # Add crown shape (7 spikes)
    draw_pixels(pixels, width, height, 'hina_crown')
    
    return pixels

def generate_kisaki_gradient(width, height, theme_name):
    """Generate Kisaki-style gradient background"""
    colors = [
        hex_to_rgb('#20265A'),  # Top
        hex_to_rgb('#171B41'),  # Mid
        hex_to_rgb('#0C0E28'),  # Bottom
    ]
    
    pixels = []
    half_height = height // 2
    
    for y in range(height):
        if y < half_height:
            t = y / half_height
            r, g, b = interpolate_color(colors[0], colors[1], t)
        else:
            t = (y - half_height) / half_height
            r, g, b = interpolate_color(colors[1], colors[2], t)
        
        pixels.extend([(r, g, b)] * width)
    
    # Add crescent moon shape
    draw_pixels(pixels, width, height, 'kisaki_crescent')
    
    return pixels

def draw_pixels(pixels, width, height, shape_type):
    """Draw halo shapes on pixel array"""
    
    center_x = width // 2
    
    if shape_type == 'hina_crown':
        # Draw 7 crown spikes
        for i in range(7):
            base_x = center_x + (20 + i * 12)
            spike_len = 80 - abs(i - 3) * 20
            alpha = 60
            
            for y_offset in range(spike_len):
                y = height // 6 - y_offset
                x = base_x
                
                if 0 <= y < height and 0 <= x < width:
                    idx = y * width + x
                    current_r, current_g, current_b = pixels[idx]
                    # Overlay white with transparency
                    pixels[idx] = (
                        min(255, current_r + alpha),
                        min(255, current_g + alpha),
                        min(255, current_b + alpha)
                    )
    
    elif shape_type == 'kisaki_crescent':
        cx, cy = center_x, height // 5
        
        # Main arc (top half)
        for angle_deg in range(0, 180, 2):
            angle = angle_deg * 3.14159 / 180
            rx, ry = 100, 60
            x = int(cx + rx * (angle - 1.57))
            y = int(cy - ry * (1 - 2 * angle_deg/180))
            
            if 0 <= y < height and 0 <= x < width:
                idx = y * width + x
                pixels[idx] = (
                    min(255, pixels[idx][0] + 80),
                    min(255, pixels[idx][1] + 80),
                    min(255, pixels[idx][2] + 80)
                )
        
        # Lower arc
        for angle_deg in range(180, 280, 2):
            angle = angle_deg * 3.14159 / 180
            rx, ry = 85, 70
            x = int(cx + rx * math.sin(angle))
            y = int(cy + ry * math.cos(angle))
            
            if 0 <= y < height and 0 <= x < width:
                idx = y * width + x
                pixels[idx] = (
                    min(255, pixels[idx][0] + 80),
                    min(255, pixels[idx][1] + 80),
                    min(255, pixels[idx][2] + 80)
                )

import math
generate_kisaki_gradient.__globals__['math'] = math

def main():
    base_dir = r'E:\project\Assets\Themes'
    
    # Generate backgrounds
    themes = {
        'Hina': {
            'background_hina.png': ('hina', generate_hina_gradient),
            'background_swimsuit.png': ('hina_swimsuit', generate_hina_gradient),
            'background_dress.png': ('hina_dress', generate_hina_gradient),
            'background_nightwear.png': ('hina_nightwear', generate_hina_gradient),
        },
        'Kisaki': {
            'background_kisaki.png': ('kisaki', generate_kisaki_gradient),
            'background_swimsuit.png': ('kisaki_swimsuit', generate_kisaki_gradient),
            'background_kindergarten.png': ('kisaki_kindergarten', generate_kisaki_gradient),
        }
    }
    
    success_count = 0
    
    for char_name, files in themes.items():
        char_dir = os.path.join(base_dir, char_name)
        os.makedirs(char_dir, exist_ok=True)
        
        for filename, (palette_key, gen_func) in files.items():
            print(f'Generating {char_name}: {filename}...')
            
            try:
                pixels = gen_func(1920, 1080, palette_key)
                
                png_data = create_png(1920, 1080, pixels)
                
                output = os.path.join(char_dir, filename)
                with open(output, 'wb') as f:
                    f.write(png_data)
                
                print(f'  ✓ Saved: {output}')
                success_count += 1
                
            except Exception as e:
                print(f'  ✗ Error: {e}')
                import traceback
                traceback.print_exc()
    
    print(f'\n=== Generation Complete ===')
    print(f'Successfully generated: {success_count}/7 backgrounds')
    print(f'Location: {base_dir}')

if __name__ == '__main__':
    main()
