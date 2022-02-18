# Marching Cubes

Implementation of the marching cubes algorithm to visualise isolines of scalar fields.  Implemented using Unity3D and multithreaded using the Unity Job System with the Unity Burst compiler.

**Currently implemented:**

- 3D marching cubes for visualising scalar fields, mesh generation performed using the Unity Job System.
- Toggleable vertex welding and normal interpolation by using dictionary lookups.
- Toggleable vertex interpolation to switch between "voxel" look and smooth look.
- Support for varying cube size and number for speed/resolution trade-off.
- Support for varying scalar field drop-off for the ball charges to control interaction range.




# Screenshots

**Metaballs**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/Metaballs.gif" width="400">

**Stream of metaballs for a fluid like effect**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/fluid.gif" width="400">

**Adding some randomness for a more natural effect**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/FluidJitter.gif" width="400">
