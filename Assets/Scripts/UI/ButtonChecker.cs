using UnityEngine;
using MM26.ECS;
using MM26.Tasks;

namespace MM26.UI
{
    public class ButtonChecker : MonoBehaviour
    {
        [SerializeField]
        private TasksManager _taskManager;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void RotX()
        {
            Debug.Log("hit x");
            TasksBatch batch = new TasksBatch();
            batch.Add(new RotationTask("1", Resources.Load<Mailbox>("Objects/Mailbox"), 90.0f, new Vector3(1, 0, 0)));
            //batch.AddTask(new RotationTask(1, 60.0f, new Vector3(1, 0, 0)));
            //batch.AddTask(new RotationTask(2, 45.0f, new Vector3(1, 0, 0)));

            _taskManager.AddTasksBatch(batch);
        }

        public void RotY()
        {
            Debug.Log("hit y");
            TasksBatch batch = new TasksBatch();
            //batch.AddTask(new RotationTask(0, 45.0f, new Vector3(0, 1, 0)));
            batch.Add(new RotationTask("1", Resources.Load<Mailbox>("Objects/Mailbox"), 60.0f, new Vector3(0, 1, 0)));
            batch.Add(new RotationTask("2", Resources.Load<Mailbox>("Objects/Mailbox"), 90.0f, new Vector3(0, 1, 0)));

            _taskManager.AddTasksBatch(batch);
        }

        public void RotZ()
        {
            Debug.Log("hit z");
            TasksBatch batch = new TasksBatch();
            //batch.AddTask(new RotationTask(0, 45.0f, new Vector3(0, 0, 1)));
            batch.Add(new RotationTask("1", Resources.Load<Mailbox>("Objects/Mailbox"), 60.0f, new Vector3(0, 0, 1)));
            batch.Add(new RotationTask("2", Resources.Load<Mailbox>("Objects/Mailbox"), 90.0f, new Vector3(0, 0, 1)));

            _taskManager.AddTasksBatch(batch);
        }

    }
}
