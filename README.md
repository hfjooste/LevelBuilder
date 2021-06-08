# Level Builder for Unity

## License
You are free to do whatever you want with this library. You're allowed to use this for commercial projects, edit the source code and even redistribute the source code. No attribution is required (but appreciated)

## Requirements
- Unity 2021.1.6f (should work on older versions as well)

## Description
This tool allows you to easily create/edit complex levels. It was designed for 3D games, but can also be used to generate 2D levels. There's two methods you can use to generate your levels:
<ol>
<li><b>Static:</b> The level is specified in the editor and can't be changed without creating a new scene</li>
<li><b>Dynamic:</b> A list of all possible levels are specified in the editor. An ID that is linked to each level is then used to generate the correct level. This method can be used to build an entire game using a single scene</li>
</ol>

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

## Generating your level (Static)
![Static Level](https://i.imgur.com/RHWAlEE.png)
<ol>
<li>Add an empty game object to your scene</li>
<li>Add the <b>Static Level Loader</b> component to the new game object</li>
<li>Specify the level that you've created in the previous section</li>
<li>Play the scene and wait for the level to be generated (should be generated instantly)</li>
</ol>

## Generating your level (Dynamic)
![Dynamic Level](https://i.imgur.com/PX48VJ1.png)
<ol>
<li>Add an empty game object to your scene</li>
<li>Add the <b>Dynamic Level Loader</b> component to the new game object</li>
<li>Add the <b>Level Index</b> component to the new game object</li>
<li>Add all the available levels to the <b>Level Index</b> component and specify a unique ID for each level</li>
<li>Play the scene and wait for the level to be generated (should be generated instantly)</li>
</ol>

<i>Please note: It's currently using PlayerPrefs to load the current level ID. But this can easily be changed</i>

## Demo
![Demo Level](https://i.imgur.com/o9He1Hz.png)

This library includes an example of a palette, level and scene. You can find this inside the <b>Demo</b> directory:
<ul>
<li><b>Data:</b> This includes an example of a palette and levels</li>
<li><b>Materials:</b> A few basic materials applied to the prefabs when generating the level</li>
<li><b>Prefabs:</b> A few basic objects that will be instantiated when generating the level</li>
<li><b>Scripts:</b> A basic demo script that allows you to change levels (dynamic only)</li>
<li><b>Static.unity:</b> A basic scene that loads the specified data and generates the level</li>
<li><b>Dynamic.unity:</b> A basic scene that contains a list of levels and generates the correct level based on the current level index</li>
</ul>
