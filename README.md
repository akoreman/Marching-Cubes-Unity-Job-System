# Marching Cubes

Implementation of the marching cubes algorithm to visualise isolines of scalar fields. Main goal is to create fluid-like effects. Implemented using Unity3D.

**Currently implemented:**

- Interpolated 3D marching cubes for visualising scalar fields (CPU).
- Vertex welding and normal interpolation by using dictionary lookups.
- Support for varying scalar field drop-off for the ball charges to control interaction range.

**To do:**

- Port to either GPU or Unity job system


# Screenshots

**Metaballs**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/Metaballs.gif" width="400">

**Stream of metaballs for a fluid like effect**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/fluid.gif" width="400">

**Adding some randomness for a more natural effect**

<img src="https://raw.github.com/akoreman/Marching-Cubes-Metaballs/main/images/FluidJitter.gif" width="400">
