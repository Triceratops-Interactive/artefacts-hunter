using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public float maxDistanceSound = 5;
        public float replaySoundDelay = 7;
        public AudioClip mummySound;
        
        
        private float _distanceTravelled;
        private float _replaySoundTime;
        private Transform _player;
        private PlayerBehaviour _playerBehaviour;

        void Start()
        {
            _player = GameObject.Find("Player").transform;
            _playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (IngameMenuBehaviour.instance.IsMenuActive() || DialogueManager.instance.IsDisplayingDialogue() || _playerBehaviour.IsDefeated())
            {
                return;
            }

            if (_replaySoundTime > 0)
            {
                _replaySoundTime -= Time.deltaTime;
            }
            if (Vector3.Distance(transform.position, _player.position) <= maxDistanceSound && _replaySoundTime <= 0)
            {
                SoundManager.instance.GetEffectSource().PlayOneShot(mummySound);
                _replaySoundTime = replaySoundDelay;
            }
            
            if (pathCreator != null)
            {
                _distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}