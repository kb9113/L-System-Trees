Create a Reperesentatuion of meshes to be used in generation

MeshData - stores the mesh itself
    * Groups currently do nothing
    * Groups are suppose to controll wheather the 2 triangles are part of the same mesh ie vertex normals are only calculated for triangles in the same group
MeshTriangle - stores a single triangle used in MeshData
IMeshPrimative - interface used by classes that create primatives for example a sphere 

MeshPrimatives
    * Sphere
    * Curved Cylendar - mabie shouldn't be in standard primatives