# Emotion
<img src="EmotionLogo.png" width="128px" />

[![Build status](https://ci.appveyor.com/api/projects/status/qur90gc2wdhmd5ff/branch/master?svg=true)](https://ci.appveyor.com/project/Cryru/emotion/branch/master)

Development: [![debug artifact](https://img.shields.io/badge/Download-%20Debug%20Build-brightgreen.svg)](https://ci.appveyor.com/api/projects/Cryru/Emotion/artifacts/EmotionCore%2Fbin%2FEmotion%20Built%20Debug.zip?branch=master&job=Configuration%3A%20Debug-GLES) [![nuget debug](https://img.shields.io/nuget/v/Emotion.svg)](https://www.nuget.org/packages/Emotion/)

Deployment: [![release artifact](https://img.shields.io/badge/Download-%20Release%20Build-brightgreen.svg)](https://ci.appveyor.com/api/projects/Cryru/Emotion/artifacts/EmotionCore%2Fbin%2FEmotion%20Built.zip?branch=master&job=Configuration%3A%20Release-GLES)

## What is it?

Emotion is a cross-platform game engine written in C#, with the intent of removing overhead and providing abstraction without sacrificing control. The idea is, to make game development about game development and not about engine or low-level backend development, but allowing those who want to do that with the option to do so. The goal is to save time, and provide indie developers (and mostly myself) with a foundation.

## Platforms Tested On:

- Windows 10 x64/x86 (Last Test On: Build 193) Nov 27th
  - Intel HD Graphics 620
  - Nvidia 940MX
  - AMD R9 200
- Ubuntu Xenial-Xerus x64 (Last Commit Tested On: 5e7bea38bb197f85376d43e164f41ce7a6a5c341) Nov 6th
- MacOS High-Sierra x64 (Last Test On: Build 193) Nov 27th

## Supported Platforms:

- Windows Vista/7/8/8.1/10 x64/x86
- Debian 9+ x64
- MacOS High-Sierra+ x64
- Android 5.0+ (Planned)

For information on how to build for other platforms check out: https://github.com/Cryru/The-Struggles-Of-Running-And-Statically-Linking-Mono

### Building Notes:

- On Linux build without a machine config, and make sure the .exe **doesn't** carry a config specifying the runtime.

## Requirements:

- OpenGL Support Options
  - ES 3 on Anything
  - 3.3 Core on MacOS
  - 3.0 Core on Windows or Linux
- GLSL Support Options
  - 300 es support on Windows or Linux
  - 300 support on MacOS
- Dynamically Uniform Expression Support Options
  - The "GL_ARB_gpu_shader5" extension
  - GLSL 400 on Windows or Linux

#### Linux

Most installations should include the proper libraries to run the engine, but some repos I've tested have some missing ones. Here is a list of ones I've found missing which the engine depends on:

- libjxr0 (https://packages.debian.org/sid/libjxr0)

- libopenjp2-7 (https://packages.debian.org/stretch/libopenjp2-7)

## Features So Far

- Window creation.
- Mouse and keyboard input.
  - Captured only while the window has focus preventing rogue clicks.
- Asset loading and management.
  - Textures: All FreeImage supported formats. ex. BMP/PNG/JPEG/GIF
  - Fonts: All FreeType supported formats. ex. TTF
  - Sounds: RIFF WAV
- Camera system.
  - Default cameras include one which follows the mouse and one which follows a target transform.
- Rendering
  - Primitives like lines and rectangles.
  - Transformation matrix.
  - Batching and buffer mapping.
  - Square textures.
  - Text.
    - Includes advanced font drawing with control over each individual glyph.
    - Richtext featuring auto wrapping, alignment, markup, and more.
- Sound engine with fading effects.
  - Play on multiple layers.
- UI system.
  - Customize base controls through inheritance or use them straight away.
  - Layouting and anchoring.
- Tiled integration and rendering.
  - Includes layer opacity, multiple tilesets, animated tiles, and more.
- An implementation of A*.
  - With the ability to add a custom heuristics function, and perform other customizations.
- A Javascript scripting engine.
- Logging and graphical debugging.
- Scenes in the form of layers. Have your UI, pause menu, levels, or anything on separate scenes.

## How to use it?

1. Download a release packet from the GitHub releases page, or compile the engine yourself.
2. Create a C# project, reference EmotionCore.dll, and setup the library files to copy on compilation. You will need them in the same folder as your executable.
3. Write a game.

For examples you can refer to the [EmotionSandbox](EmotionSandbox) project.

## Projects Used

OpenTK [OpenGL/OpenAL] : Context and host creation, input capturing, GL API and AL api.
- OpenAL32.dll included. x64/x86
- openal.so included. x64
    - libsndio.so.6.1 included.

FreeImage-DotNet-Core [FreeImage] : Loading and converting images.
- FreeImage.dll included. x64/x86
- FreeImage.so included. x64
    - libpng14.14.dylib included.
- FreeImage.dylib included. x64

SharpFont [FreeType] : Loading fonts and glyphs, and rendering them.
- freetype6.dll included. x64/x86
- freetype.so included. x64
- libfreetype.6.dylib included. x64

TiledSharp [Modified] : Loading .tmx Tiled files.

Jint : Javascript script engine.

Soul : Logging.

## Inspired By

- Processing
- MonoGame
- LOVE Framework
