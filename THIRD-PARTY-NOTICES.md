# Third-party notices

This project vendors third-party code under `Assets/SuperTiled2Unity/`. Those files
are covered by their own licenses, not by this repository's license, and are
redistributed here unmodified.

## SuperTiled2Unity

Tiled (`.tmx` / `.tsx`) map importer for Unity, by Sean Barton.
Upstream: https://github.com/Seanba/SuperTiled2Unity

The copy vendored here does not include an upstream license file. Refer to the
upstream repository for the terms that apply to this component.

SuperTiled2Unity in turn bundles the following libraries, each under its own
license, with the original headers left intact in the source files:

### Clipper (v6.4.2)

`Assets/SuperTiled2Unity/Scripts/Editor/ThirdParty/clipper.cs`

Copyright © Angus Johnson 2010–2017. Licensed under the
**Boost Software License, Version 1.0** — http://www.boost.org/LICENSE_1_0.txt

### LibTessDotNet

`Assets/SuperTiled2Unity/Scripts/Editor/ThirdParty/LibTessDotNet/`

Copyright © 2011 Silicon Graphics, Inc. Licensed under the
**SGI Free Software License B, Version 2.0**. Original copyright and permission
notices are preserved in each file.

### Other bundled helpers

`MaxRectsBinPack.cs`, `MultiValueDictionary.cs` and `ImageHeader.cs` under the same
`ThirdParty/` directory carry their own attribution headers in-file.

## Unity packages

Packages listed in `Packages/manifest.json` are resolved by the Unity Package
Manager at import time and are governed by the Unity Package Distribution terms.
They are not redistributed in this repository.
