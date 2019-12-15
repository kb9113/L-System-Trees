Create a Reperesentatuion of meshes to be used in generation

MeshData - stores the mesh itself
MeshTriangle - stores a single triangle used in MeshData
IMeshPrimative - interface used by classes that create primatives for example a sphere 

## TODO
* Groups currently do nothing
* Groups are suppose to controll wheather the 2 triangles are part of the same mesh ie vertex normals are only calculated for triangles in the same group
* Allow GetMesh method to return an array of meshes if the number of vertices exceeds the max

## MeshPrimatives
* Sphere
* Curved Cylendar - mabie shouldn't be in standard primatives