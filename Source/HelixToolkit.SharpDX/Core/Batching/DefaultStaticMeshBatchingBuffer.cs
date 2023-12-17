﻿using HelixToolkit.SharpDX.Model;
using HelixToolkit.SharpDX.Utilities;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace HelixToolkit.SharpDX.Core;

public class DefaultStaticMeshBatchingBuffer
    : StaticGeometryBatchingBufferBase<BatchedMeshGeometryConfig, BatchedMeshVertex>
{
    private PhongMaterialCore[]? materials;
    public PhongMaterialCore[]? Materials
    {
        set
        {
            if (Set(ref materials, value))
            {
                InvalidateGeometries();
            }
        }
        get
        {
            return materials;
        }
    }

    public DefaultStaticMeshBatchingBuffer(PrimitiveTopology topology, IElementsBufferProxy vertexBuffer, IElementsBufferProxy indexBuffer)
        : base(topology, vertexBuffer, indexBuffer)
    {

    }

    public DefaultStaticMeshBatchingBuffer()
        : base(PrimitiveTopology.TriangleList,
             new ImmutableBufferProxy(BatchedMeshVertex.SizeInBytes, BindFlags.VertexBuffer),
             new ImmutableBufferProxy(sizeof(int), BindFlags.IndexBuffer))
    {

    }

    protected override void OnFillVertArray(BatchedMeshVertex[] array, int offset, ref BatchedMeshGeometryConfig geometry, ref Matrix transform)
    {
        if (Materials == null)
        {
            return;
        }
        else if (geometry.Geometry is MeshGeometry3D mesh && mesh.Positions is not null)
        {
            var materialCount = Materials.Length;
            var vertexCount = mesh.Positions.Count;
            var positions = mesh.Positions.GetEnumerator();
            var normals = mesh.Normals != null ? mesh.Normals.GetEnumerator() : Enumerable.Repeat(Vector3.Zero, vertexCount).GetEnumerator();
            var tangents = mesh.Tangents != null ? mesh.Tangents.GetEnumerator() : Enumerable.Repeat(Vector3.Zero, vertexCount).GetEnumerator();
            var bitangents = mesh.BiTangents != null ? mesh.BiTangents.GetEnumerator() : Enumerable.Repeat(Vector3.Zero, vertexCount).GetEnumerator();
            var textures = mesh.TextureCoordinates != null ? mesh.TextureCoordinates.GetEnumerator() : Enumerable.Repeat(Vector2.Zero, vertexCount).GetEnumerator();
            var material = Materials[geometry.MaterialIndex < materialCount ? geometry.MaterialIndex : 0];

            var diffuse = material.DiffuseColor.EncodeToFloat();
            var emissive = material.EmissiveColor.EncodeToFloat();
            var specular = material.SpecularColor.EncodeToFloat();
            var reflect = material.ReflectiveColor.EncodeToFloat();
            var ambient = material.EmissiveColor.EncodeToFloat();
            var colorEncode = new Vector4(diffuse, emissive, specular, reflect);
            var colorEncode2 = new Vector4(ambient, material.SpecularShininess, material.DiffuseColor.Alpha, 0);

            if (transform == Matrix.Identity)
            {
                for (var i = offset; i < offset + vertexCount; ++i)
                {
                    positions.MoveNext();
                    normals.MoveNext();
                    tangents.MoveNext();
                    bitangents.MoveNext();
                    textures.MoveNext();
                    array[i] = new BatchedMeshVertex()
                    {
                        Position = positions.Current.ToVector4(),
                        Normal = normals.Current,
                        Tangent = tangents.Current,
                        BiTangent = bitangents.Current,
                        TexCoord = textures.Current,
                        Color = colorEncode,
                        Color2 = colorEncode2
                    };
                }
            }
            else
            {
                for (var i = offset; i < offset + vertexCount; ++i)
                {
                    positions.MoveNext();
                    normals.MoveNext();
                    tangents.MoveNext();
                    bitangents.MoveNext();
                    textures.MoveNext();
                    array[i] = new BatchedMeshVertex()
                    {
                        Position = positions.Current.ToVector4().Transform(ref transform),
                        Normal = Vector3Helper.TransformNormal(normals.Current, ref transform),
                        Tangent = Vector3Helper.TransformNormal(tangents.Current, ref transform),
                        BiTangent = Vector3Helper.TransformNormal(bitangents.Current, ref transform),
                        TexCoord = textures.Current,
                        Color = colorEncode,
                        Color2 = colorEncode2
                    };
                }
            }

            positions.Dispose();
            normals.Dispose();
            tangents.Dispose();
            bitangents.Dispose();
            textures.Dispose();
        }
    }
}
