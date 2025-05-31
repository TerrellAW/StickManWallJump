## Description
StickManWallJump is an upcoming mobile game where wall jumping is a central mechanic.

## Development Environment Setup
### Prerequisites
**1. Install .NET SDK** - Download and install the latest .NET SDK from dotnet.microsoft.com

**2. Install MonoGame SDK** - Download and install the MonoGame SDK from monogame.net/downloads
### Project Setup
**1. Clone Repository**
```
git clone git@github.com:TerrellAW/StickManWallJump.git
cd StickManWallJump
```
**2. Restore Dependencies**
```
dotnet restore
dotnet tools restore
```
**3. Build Project**
```
dotnet build
```
**4. Build Content**
```
dotnet mgcb /build:Content/Content.mgcb
```
- Alternatively, you can open the content editor to manage assets:
  - **General**: `dotnet mgcb-editor Content/Content.mgcb`
  - **Linux**: `dotnet mgcb-editor-linux Content/Content.mgcb`
  - **Mac**: `dotnet mgcb-editor-mac Content/Content.mgcb`
- Ensure all textures are loaded into the content loader
### Troubleshooting
- If textures don't load verify the Content directory structure. Images used for textures should be in the `Content/Textures/` subdirectory.
- Check that file names match exactly.
- Ensure MonoGame content has been properly built.
### Debug Mode
To run debug mode use this command:
```
dotnet run -- --debug
```
Or run:
```
dotnet run -- -d
```
In a version compiled for windows the command would be:
```
StickManWallJump.exe --debug
```
Or run:
```
StickManWallJump.exe -d
```
