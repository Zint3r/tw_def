using Game.Features.Enemy;
using Game.Features.GameDesign.DefinitionObjects.Enemyes;
using Game.Features.GameDesign.DefinitionObjects.Map;
using Game.Features.MovePoints;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Zenject;

namespace Game.Features.GameDesign.Parsers
{
	public class EnemyDefinitionParser
	{
        [Inject]
        private GameDefinitionModel gameDefinitionModel;
        
        [Inject] 
        private IEnemySpawnerService enemySpawnerService;
        
        public IEnumerator Initialize()
        {
			Dictionary<string, List<EnemyWaveVO>> enemyWaves = new Dictionary<string, List<EnemyWaveVO>>
			{
				{
					MapConstants.Aden,
					new List<EnemyWaveVO>
					{
						new EnemyWaveVO
						{
							WaveId = 1,
							SpawnTimer = 3f,
							EnemyList = new List<EnemyVO>()
							{
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_3",
									Speed = 4f,
									Hp = 10,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_3"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_3",
									Speed = 4f,
									Hp = 10,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_3"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_2",
									Speed = 6f,
									Hp = 5,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_2"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_1",
									Speed = 7f,
									Hp = 15,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_1"),
									MovePoints = new List<MovePointVO>
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
						new EnemyWaveVO
						{
							WaveId = 2,
							SpawnTimer = 1f,
							EnemyList = new List<EnemyVO>()
							{
								new EnemyVO
								{
									Id = 0,
									Name = "empty",
									Speed = 1f,
									Hp = 100,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("empty"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_1",
									Speed = 7f,
									Hp = 15,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_1"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_1",
									Speed = 7f,
									Hp = 15,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_1"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_2",
									Speed = 6f,
									Hp = 5,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_2"),
									MovePoints = new List<MovePointVO>
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
								},
								new EnemyVO
								{
									Id = 0,
									Name = "enemy_aden_type_2",
									Speed = 6f,
									Hp = 5,
									InControl = false,
									IsAlive = true,
									MoveIndex = 0,
									EnemyDefinition = gameDefinitionModel.GetOrCreate<EnemyDefinition>("enemy_aden_type_2"),
									MovePoints = new List<MovePointVO>
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

						}
					}
				},
				{
					MapConstants.Dion,
					new List<EnemyWaveVO>
					{
						new EnemyWaveVO
						{

						},
						new EnemyWaveVO
						{

						}
					}
				}
			};

			enemySpawnerService.InitializeEnemyWaves(enemyWaves);
            yield break;
        }		
	}
}