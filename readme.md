This code is based on [the CityBuilder tutorial](https://www.binpress.com/tutorial/creating-a-city-building-game-with-sfml/137) by [Daniel Mansfield](https://github.com/Piepenguin1995). 

To brush up on my C++ skills, I followed the tutorial and ported to C# on-the-fly. That means it may still contain some C++-isms since I did not get into refactoring the code, just translating, almost line-by-line. For example, Map's Load() and Save() methods should be using serialization instead of manually reading/writing bytes. 

The primary differences come about from using 
the [SFML.NET](http://www.sfml-dev.org/download/sfml.net/) bindings for SFML. This allowed for some reduced verbosity thanks to .NET and also eliminates the use of SFML's event system (so, for example, RenderWindow.DispatchEvents() is used instead of calling HandleInput() on each GameState).

To ease development/linking, I used [Graphnode's Nuget Package](https://www.nuget.org/packages/Graphnode.SFML.Net/) instead of the official SFML package. The Graphnode package includes [the C bindings for SFML](http://www.sfml-dev.org/download/csfml/), which are required for SFML.Net, and uses SFML.NET 2.3 instead of the current official release, 2.2.

So far, it implements the code through [Part 5](https://www.binpress.com/tutorial/creating-a-city-building-game-with-sfml-part-5-the-game-world/127).

This code maintains the original's MIT license.