using System.Collections.Generic;
using UnityEngine;

namespace stickin.mathcross
{
    [System.Serializable]
    public class DifficultParams
    {
        public int DifficultNumber;
        public Vector2Int Size;
        public int MaxNumber;
        [Range(0, 100)]
        public int PercentageNumbersToPocket;
        [Range(0, 100)]
        public int PercentageRewards;
    }
    
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Stickin/Math Cross/Game Config")]
    public class MathCrossGameConfig : GameConfig
    {
        // [Header("MathCross params:")]
        public List<DifficultParams> DifficultConfigs;

        public DifficultParams GetDifficultConfig(int difficultNumber)
        {
            foreach (var config in DifficultConfigs)
            {
                if (config.DifficultNumber == difficultNumber)
                    return config;
            }

            return DifficultConfigs.GetRandom();
        }
    }
}