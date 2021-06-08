# Level Builder for Unity

## License
You are free to do whatever you want with this library. You're allowed to use this for commercial projects, edit the source code and even redistribute the source code. No attribution is required (but appreciated)

## Requirements
- Unity 2021.1.6f (should work on older versions as well)

## Creating a new Palette
![Palette Example](https://i.imgur.com/bawI02F.png)
<ol>
<li>Right click in the project view</li>
<li>Go to Create > Level Builder > Palette</li>
<li>Add all the items you'll need for your level to this file</li>
<ul>
<li><b>Name:</b> The name displayed when creating new levels. This is only used to help you identify which tiles you're applying and will not be displayed to the player</li>
<li><b>ID:</b> An ID for this palette item. This is used to load the correct data when editing/generating levels and should be unique for each item in the palette</li>
<li><b>Color:</b> The color used to represent this item when editing levels. This color is not used in the actual level</li>
</li><b>Prefab:</b> The prefab that will be instantiated when generating the level using this palette item</li>
<li><b>Offset:</b> The offset applied to the instance when generating the level using this palette item</li>
</ul>
</ol>

## Creating a new Level
![Level Example](https://i.imgur.com/iy7U386.png)
<ol>
<li>Right click in the project view</li>
<li>Go to Create > Level Builder > Level</li>
<li>Specify the palette you've created in the previous section</li>
<li>Specify the size of the level (Size X and Size Y)</li>
<li>Start designing your level by selecting an item from the dropdown list and clicking on the empty tiles</li>
</ol>

## Generating your level
![Generated Level](https://i.imgur.com/o9He1Hz.png)
<ol>
<li>Add an empty game object to your scene</li>
<li>Add the <b>Level Loader</b> component to the new game object</li>
<li>Specify the level that you've created in the previous section</li>
<li>Play the scene and wait for the level to be generated (should be generated instantly)</li>
</ol>

## Demo
This library includes an example of a palette, level and scene. You can find this inside the <b>Demo</b> directory:
<ul>
<li><b>Data:</b> This include an example of a palette and level</li>
<li><b>Materials:</b> A few basic materials applied to the prefabs when generating the level</li>
<li><b>Prefabs:</b> A few basic objects that will be instantiated when generating the level</li>
<li><b>Demo.unity:</b> A basic scene that loads the saved data and generate a level</li>
</ul>