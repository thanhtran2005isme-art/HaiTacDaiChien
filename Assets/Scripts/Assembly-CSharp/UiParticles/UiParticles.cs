using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UiParticles
{
	[RequireComponent(typeof(ParticleSystem))]
	public class UiParticles : MaskableGraphic
	{
		private struct Geometry
		{
			private NativeArray<Vector3> _positions;

			private NativeArray<Vector2> _uvs;

			private NativeArray<Color32> _colors;

			private NativeArray<Vector2> _secondaryUvs;

			private NativeArray<Vector4> _tangents;

			private NativeArray<Vector3> _normals;

			private NativeArray<ushort> _indices;

			private int _verticesCount;

			private int _indicesCount;

			public bool IsEmpty => false;

			public NativeSlice<Vector3> Positions => default;

			public NativeSlice<Vector2> Uvs => default;

			public NativeSlice<ushort> Indices => default;

			public NativeSlice<Color32> Colors => default;

			public void Reset()
			{
			}

			public void SetVerticesCount(int vertCount)
			{
			}

			public void SetIndicesCount(int indCount)
			{
			}

			public void Dispose()
			{
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void DisposeVertices()
			{
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void DisposeIndices()
			{
			}

			public bool Expose(Mesh mesh)
			{
				return false;
			}

			private static void Fill<T>(ref NativeArray<T> slice, int count, T value) where T : struct
			{
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static int NextPow2(int i)
			{
				return 0;
			}
		}

		[FormerlySerializedAs("m_ParticleSystem")]
		[SerializeField]
		private ParticleSystem m_ParticleSystem;

		[FormerlySerializedAs("m_RenderMode")]
		[SerializeField]
		[Tooltip("Render mode of particles")]
		private UiParticleRenderMode m_RenderMode;

		[Tooltip("Speed Scale for streched billboards")]
		[FormerlySerializedAs("m_StretchedSpeedScale")]
		[SerializeField]
		private float m_StretchedSpeedScale;

		[Tooltip("Speed Scale for streched billboards")]
		[SerializeField]
		[FormerlySerializedAs("m_StretchedLenghScale")]
		private float m_StretchedLenghScale;

		[Tooltip("If true, particles ignore timescale")]
		[SerializeField]
		[FormerlySerializedAs("m_IgnoreTimescale")]
		private bool m_IgnoreTimescale;

		[SerializeField]
		private Mesh m_RenderedMesh;

		private const string MainTexPropertyName = "_MainTex";

		private const string BaseMapPropertyName = "_BaseMap";

		private int m_MainTexProperty;

		private Geometry _geometry;

		private ParticleSystemRenderer m_ParticleSystemRenderer;

		private ParticleSystem.Particle[] m_Particles;

		private Mesh _cachedMesh;

		private Vector3[] m_MeshVerts;

		private int[] m_MeshTriangles;

		private Vector2[] m_MeshUvs;

		public ParticleSystem ParticleSystem
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public override Texture mainTexture => null;

		public UiParticleRenderMode RenderMode
		{
			get
			{
				return UiParticleRenderMode.Billboard;
			}
			set
			{
			}
		}

		public Mesh RenderedMesh
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		protected override void Awake()
		{
		}

		private void InitMeshData()
		{
		}

		public override void SetMaterialDirty()
		{
		}

		protected override void UpdateGeometry()
		{
		}

		protected override void OnDestroy()
		{
		}

		protected virtual void Update()
		{
		}

		private void InitParticlesBuffer(ParticleSystem.MainModule mainModule)
		{
		}

		private void GenerateParticlesBillboards()
		{
		}

		private void DrawParticleMesh(ParticleSystem.Particle particle, int particleIndex, ParticleSystem.MinMaxCurve frameOverTime, bool isWorldSimulationSpace, ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule, Vector3[] verts, int[] triangles, Vector2[] uvs)
		{
		}

		private void DrawParticleBillboard(ParticleSystem.Particle particle, int particleIndex, ParticleSystem.MinMaxCurve frameOverTime, ParticleSystem.MinMaxCurve velocityOverTimeX, ParticleSystem.MinMaxCurve velocityOverTimeY, ParticleSystem.MinMaxCurve velocityOverTimeZ, bool isWorldSimulationSpace, ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule)
		{
		}

		private static void CalculateUvs(ParticleSystem.Particle particle, ParticleSystem.MinMaxCurve frameOverTime, ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule, float timeAlive, out Vector2 uv0, out Vector2 uv1, out Vector2 uv2, out Vector2 uv3)
		{
			uv0 = default;
			uv1 = default;
			uv2 = default;
			uv3 = default;
		}

		private void GetStrechedBillboardsSizeAndRotation(ParticleSystem.Particle particle, float timeAlive01, ref Vector3 size3D, out Quaternion rotation, ParticleSystem.MinMaxCurve x, ParticleSystem.MinMaxCurve y, ParticleSystem.MinMaxCurve z)
		{
			rotation = default;
		}
	}
}
