using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PF.Managers
{
    public class GroundTilesManager : MonoBehaviour
    {
        private GameObject _player;
        private Tilemap _tilemap;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _tilemap = GetComponent<Tilemap>();
        }

        private void Update()
        {
            // Get the player position
            var playerPosition = _player.transform.position;
            
            // Get the tile at the player position
            var playerTilePosition = _tilemap.WorldToCell(playerPosition);
            
            // Get adjacent tiles
            var adjacentTilePositions = new List<Vector3Int>(8);
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var adjacentTilePosition = playerTilePosition + new Vector3Int(x, y, 0);
                    if (adjacentTilePosition == playerTilePosition) continue;
                    adjacentTilePositions.Add(adjacentTilePosition);
                }
            }
            
            // Change the color of the adjacent tiles
            foreach (var adjacentTilePosition in adjacentTilePositions)
            {
                var tile = _tilemap.GetTile(adjacentTilePosition);
                if (tile == null) continue;
                _tilemap.SetTileFlags(adjacentTilePosition, TileFlags.None);
                _tilemap.SetColor(adjacentTilePosition, Color.black);
            }
        }
    }
}
