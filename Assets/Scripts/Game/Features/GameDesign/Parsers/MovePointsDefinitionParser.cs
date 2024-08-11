using Game.Features.GameDesign.DefinitionObjects.Map;
using Game.Features.MovePoints;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Zenject;

namespace Game.Features.GameDesign.Parsers
{
	public class MovePointsDefinitionParser
	{
        [Inject]
        private GameDefinitionModel gameDefinitionModel;
        
        [Inject] 
        private IMovePointsService movePointsService;
        
        public IEnumerator Initialize()
        {
			Dictionary<string, Dictionary<int, List<MovePointVO>>> points = new Dictionary<string, Dictionary<int, List<MovePointVO>>>
			{
				{
					MapConstants.Aden,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(45f, 0f, -45f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								},
								new MovePointVO
								{
									PointPosition = new float3(25f, 0f, -45f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								},
								new MovePointVO
								{
									PointPosition = new float3(25f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								},
								new MovePointVO
								{
									PointPosition = new float3(-15f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								},
								new MovePointVO
								{
									PointPosition = new float3(-15f, 0f, -28f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								},
								new MovePointVO
								{
									PointPosition = new float3(-45f, 0f, -28f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								},
								new MovePointVO
								{
									PointPosition = new float3(-45f, 0f, -45f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Aden)
								}
							}
						}
					}
				},
				{
					MapConstants.Dion,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Dion)
								},
								new MovePointVO
								{
									PointPosition = new float3(25f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Dion)
								},
								new MovePointVO
								{
									PointPosition = new float3(30f, 0f, 30f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Dion)
								},
								new MovePointVO
								{
									PointPosition = new float3(35f, 0f, 35f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Dion)
								},
								new MovePointVO
								{
									PointPosition = new float3(40f, 0f, 40f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Dion)
								}
							}
						}
					}
				},
				{
					MapConstants.Giran,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Giran)
								},
								new MovePointVO
								{
									PointPosition = new float3(-20f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Giran)
								},
								new MovePointVO
								{
									PointPosition = new float3(-30f, 0f, 10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Giran)
								},
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 30f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Giran)
								},
								new MovePointVO
								{
									PointPosition = new float3(-20f, 0f, 20f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Giran)
								}
							}
						}
					}
				},
				{
					MapConstants.Goddard,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Goddard)
								},
								new MovePointVO
								{
									PointPosition = new float3(20f, 0f, -10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Goddard)
								},
								new MovePointVO
								{
									PointPosition = new float3(30f, 0f, -10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Goddard)
								},
								new MovePointVO
								{
									PointPosition = new float3(40f, 0f, -10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Goddard)
								},
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, -10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Goddard)
								}
							}
						}
					}
				},
				{
					MapConstants.Heine,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Heine)
								},
								new MovePointVO
								{
									PointPosition = new float3(25f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Heine)
								},
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Heine)
								},
								new MovePointVO
								{
									PointPosition = new float3(25f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Heine)
								},
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Heine)
								}
							}
						}
					}
				},
				{
					MapConstants.Oren,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Oren)
								},
								new MovePointVO
								{
									PointPosition = new float3(30f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Oren)
								},
								new MovePointVO
								{
									PointPosition = new float3(35f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Oren)
								},
								new MovePointVO
								{
									PointPosition = new float3(35f, 0f, 10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Oren)
								},
								new MovePointVO
								{
									PointPosition = new float3(35f, 0f, 20f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Oren)
								}
							}
						}
					}
				},
				{
					MapConstants.Rune,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Rune)
								},
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Rune)
								},
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 50f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Rune)
								},
								new MovePointVO
								{
									PointPosition = new float3(10f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Rune)
								},
								new MovePointVO
								{
									PointPosition = new float3(25f, 0f, 25f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Rune)
								}
							}
						}
					}
				},
				{
					MapConstants.Schuttgart,
					new Dictionary<int, List<MovePointVO>>
					{
						{
							0,
							new List<MovePointVO>
							{
								new MovePointVO
								{
									PointPosition = new float3(0f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Schuttgart)
								},
								new MovePointVO
								{
									PointPosition = new float3(-20f, 0f, 0f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Schuttgart)
								},
								new MovePointVO
								{
									PointPosition = new float3(-40f, 0f, 10f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Schuttgart)
								},
								new MovePointVO
								{
									PointPosition = new float3(-30f, 0f, 20f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Schuttgart)
								},
								new MovePointVO
								{
									PointPosition = new float3(-40f, 0f, 30f),
									MapDefinition = gameDefinitionModel.GetOrCreate<MapDefinition>(MapConstants.Schuttgart)
								}
							}
						}
					}
				}
			};
			movePointsService.InitializeMovePoints(points);
            yield break;
        }
    }
}