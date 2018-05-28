# MapCombiner
This is a quick and dirty C# application which allows combining map tile images
together into a single map which can be exported as a PNG.

MapCombiner has an image browser on the left and the generated map on the right.
The map tile highlighted in yellow is the current tile. To edit a map, do the following:
  - The generated map is made up of square map tiles, which will all be output in the
    resulting map at the same resolution. Choose "Edit > Settings..." to change the
    tile count and output size of a tile in pixels.
  - Choose "File > Add Images..." and select one or more images to add
    to the image browser.
  - Click or use the arrow keys to change which is the current tile.
  - Click on an image in the image browser to assign an image to the current
    tile.
  - Right click on a tile to rotate the placed tile 90 degrees.
  - Use the mouse wheel to zoom the map view in and out (this does not affect
    the export resolution).
    
