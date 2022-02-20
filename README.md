# Marching Cubes

Implementation of the marching cubes algorithm to visualise isolines of scalar fields.  Implemented using Unity3D and multithreaded using the Unity Job System with the Unity Burst compiler. For the fluid stream benchmark scene using the jobsystem increases the framerate from +/- 25 fps to +/- 85 fps on an Intel i7 11700 CPU. Currently the scripts are setup to recalculate the complete mesh every frame for benchmarking purposes.

**Currently implemented:**

- 3D marching cubes for visualising scalar fields, mesh generation optimization by using the Unity C# Job System and Burst compiler.
- Toggleable vertex welding and normal interpolation by using dictionary lookups.
- Toggleable vertex interpolation to switch between "voxel" look and smooth look.
- Support for varying cube size and number for speed/resolution trade-off.
- Support for varying scalar field drop-off for the ball charges to control interaction range.




# Screenshots

**Metaballs**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/Metaballs.gif" width="400">

**Stream of metaballs for a fluid like effect**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/FluidJitter.gif" width="400">  

**The two most resource-intensive tasks scheduled to the pool of workers**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/profiler.png" width="400"> 
