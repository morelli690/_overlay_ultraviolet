_For questions and discussion, check out our [Gitter](https://gitter.im/ultraviolet-framework/Lobby)._

### Builds
| Branch       | Integration | Release |
|--------------|-------------|---------|
| master       | ![Build Status](http://dev.twistedlogik.net:8085/plugins/servlet/wittified/build-status/UV-INT)  | ![Build Status](http://dev.twistedlogik.net:8085/plugins/servlet/wittified/build-status/UV-REL) |
| develop      | ![Build Status](http://dev.twistedlogik.net:8085/plugins/servlet/wittified/build-status/UV-INT4)  | ![Build Status](http://dev.twistedlogik.net:8085/plugins/servlet/wittified/build-status/UV-REL2) |

What is Ultraviolet?
====================

[![Join the chat at https://gitter.im/ultraviolet-framework/Lobby](https://badges.gitter.im/ultraviolet-framework/Lobby.svg)](https://gitter.im/ultraviolet-framework/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Ultraviolet is a cross-platform, .NET game development framework written in C# and released under the [MIT License](http://opensource.org/licenses/MIT). It is heavily inspired by Microsoft's XNA Framework, and is intended to be easy for XNA developers to quickly pick up and start using. However, unlike [MonoGame](http://www.monogame.net/) and similar projects, Ultraviolet is not intended to be a drop-in replacement for XNA. Its current implementation is written on top of [SDL2](https://www.libsdl.org/) and [OpenGL](https://www.opengl.org/), but its modular design makes it (relatively) easy to re-implement using other technologies if it becomes necessary to do so in the future.

At present, Ultraviolet officially supports Windows, Linux, macOS, iOS, and Android.

Some core features of the Ultraviolet Framework:

 * __A runtime content pipeline__
   
   Easily load game assets using Ultraviolet's content pipeline. Unlike XNA, Ultraviolet's content pipeline operates at runtime, meaning no special Visual Studio projects are required to make it work. Content preprocessing is supported in order to increase efficiency and decrease load times.
 
 * __High-level 2D rendering abstractions__
   
   Familiar classes like SpriteBatch allow you to efficiently render large numbers of 2D sprites. Ultraviolet includes built-in support for texture atlases and XML-driven sprite sheets.
 
 * __High-level 3D rendering abstractions__
   
   Coming in a future release.
 
 * __Low-level rendering functionality__
   
   In addition to the abstractions described above, Ultraviolet's graphics subsystem allows you to push polygons directly to the graphics device, giving you complete control.
 
 * __A powerful text formatting and layout engine__
   
   Do more than draw plain strings of text. Ultraviolet's text formatting engine allows you to change your text's font, style, and color on the fly. The layout engine allows you to easily position and align text wherever you need it.
 
 * __XML-driven object loader for easy content creation__
   
   Ultraviolet's object loader allows you to easily create complicated hierarchies of objects from simple XML files. This is more than just an XML serializer&mdash;because it is integrated with Ultraviolet, it has direct knowledge of your game's content assets and object lists, making it possible to reference them in a simple, flexible, and readable way.

The Ultraviolet Framework's source code is [available on GitHub](https://github.com/tlgkccampbell/ultraviolet). If you're developing on Windows, and you just want to get started making games, you can use the installer provided as part of the [latest release](https://github.com/tlgkccampbell/ultraviolet/releases). It will ensure that you have the necessary DLLs and install Visual Studio templates for developing Ultraviolet applications. 

Getting Started
===============

If you don't want to build Ultraviolet yourself, official packages are available through [NuGet](https://www.nuget.org/packages?q=ultraviolet). There are packages for each of Ultraviolet's individual libraries, as well as several packages which aggregate all of the packages required by a particular target platform.

The wiki contains a quick start guide for development on both [Windows](https://github.com/tlgkccampbell/ultraviolet/wiki/Getting-Started-on-Windows) and [macOS](https://github.com/tlgkccampbell/ultraviolet/wiki/Getting-Started-on-macOS).

A [dedicated repository](https://github.com/tlgkccampbell/ultraviolet-samples) contains a number of sample projects which demonstrate various features of the Framework.

Requirements
============

Using Ultraviolet requires that your project is targeted against .NET Framework 4.7 or later, or an equivalent Mono or Xamarin runtime.

Building Ultraviolet requires Visual Studio 2017 or later, or an equivalent version of the Mono development tools.

Building the mobile projects requires the appropriate Xamarin tools to be installed.

The following platforms are supported for building the Framework:
* Windows
* Linux (Ubuntu)
* Android
* macOS
* iOS

Please file an issue if you encounter any difficulty building on any of these platforms. Linux distributions other than Ubuntu should work, assuming that they can run Mono and you can provide appropriate versions of the native dependencies, but only Ubuntu has been thoroughly tested.

Building
========

__Desktop Platforms__

The `Sources` folder contains several solution files for the various platforms which Ultraviolet supports. Alternatively, you can run `msbuild Ultraviolet.proj` from the command line in the repository's root directory; this will automatically select and build the correct solution for your current platform, and additionally will copy the build results into a single `Binaries` folder.

__Mobile Platforms__

Building Ultraviolet for iOS and Android requires that Xamarin be installed. As with the desktop version of the Framework, you can either build the appropriate solution file or `Ultraviolet.proj`, but in the latter case you must also explicitly specify that you want to use one of the mobile build targets, i.e.:

    msbuild Ultraviolet.proj /t:BuildAndroid
    
or

    msbuild Ultraviolet.proj /t:BuildiOS
