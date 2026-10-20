#!/usr/bin/env python3
"""Generate placeholder backgrounds for Blue Archive character themes"""

import os
from PIL import Image, ImageDraw

base_dir = r'E:\project\Assets\Themes'

palettes = {
    'hina': ['#3E1249', '#2A0D33', '#17061D'],
    'hina_swimsuit': ['#0F4E7C', '#0B3A5C', '#05202F'],
    'hina_dress': ['#382A6B', '#291E51', '#150E2C'],
    'hina_nightwear': ['#3C2C1C', '#2B1F13', '#19110A'],
    'kisaki': ['#20265A', '#171B41', '#0C0E28'],
    'kisaki_swimsuit': ['#3C3C58', '#2B2B42', '#17172B'],
    'kisaki_kindergarten': ['#56411F', '#3C2D15', '#221909'],
}

def hex_to_rgb(hex_color):
    return tuple(int(hex_color[i:i+2], 16) for i in (0, 2, 4))

def create_gradient(width, height, palette_key):
    """Create vertical gradient with halo shapes"""
    colors = palettes[palette_key]
    c1 = hex_to_rgb(colors[0])
    c2 = hex_to_rgb(colors[1])
    c3 = hex_to_rgb(colors[2])
    
    img = Image.new('RGB', (width, height))
    pixels = []
    
    for y in range(height):
        if y < height // 2:
            ratio = y / (height // 2)
            r = int(c1[0] + (c2[0] - c1[0]) * ratio)
            g = int(c1[1] + (c2[1] - c1[1]) * ratio)
            b = int(c1[2] + (c2[2] - c1[2]) * ratio)
        else:
            ratio = (y - height // 2) / (height // 2)
            r = int(c2[0] + (c3[0] - c2[0]) * ratio)
            g = int(c2[1] + (c3[1] - c2[1]) * ratio)
            b = int(c2[2] + (c3[2] - c2[2]) * ratio)
        
        pixels.extend([(r, g, b)] * width)
    
    img.putdata(pixels)
    draw = ImageDraw.Draw(img)
    
    # Add halo shape based on character
    w, h = width, height
    center_x = w // 2
    
    if 'hina' in palette_key.lower():
        # Crown with 7 spikes
        for i in range(7):
            base_x = center_x + (20 + i * 12)
            spike_len = 80 - abs(i - 3) * 20
            y1 = h // 5
            y2 = h // 5 - spike_len
            draw.line([(base_x, y1), (base_x, y2)], fill=(255, 255, 255, 60), width=5)
    
    elif 'kisaki' in palette_key.lower():
        # Crescent moon + droplet
        cx, cy = w // 2, h // 5
        draw.arc([cx - 100, cy - 30, cx + 100, cy + 50], 0, 180, fill=(255, 255, 255, 80), width=10)
        draw.arc([cx - 85, cy - 10, cx + 85, cy + 70], 180, 280, fill=(255, 255, 255, 80), width=10)
        
        # Small droplet
        draw.ellipse([cx + 25, cy + 60, cx + 45, cy + 80], fill=(255, 255, 255, 60))
    
    return img

# Generate all backgrounds
for char, files in [('Hina', [('background_hina.png', 'hina'), 
                               ('background_swimsuit.png', 'hina_swimsuit'),
                               ('background_dress.png', 'hina_dress'),
                               ('background_nightwear.png', 'hina_nightwear')]),
                    ('Kisaki', [('background_kisaki.png', 'kisaki'),
                               ('background_swimsuit.png', 'kisaki_swimsuit'),
                               ('background_kindergarten.png', 'kisaki_kindergarten'])]):
    
    char_dir = os.path.join(base_dir, char)
    os.makedirs(char_dir, exist_ok=True)
    
    for filename, palette_key in files:
        print(f'Generating {char}: {filename}...')
        
        try:
            img = create_gradient(1920, 1080, palette_key)
            output = os.path.join(char_dir, filename)
            img.save(output, 'PNG')
            print(f'  ✓ Saved: {output}')
        except Exception as e:
            print(f'  ✗ Error: {e}')

print('\n=== Generation Complete ===')
