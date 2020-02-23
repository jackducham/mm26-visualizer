using System;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : Singleton<Mailbox>
{
	private IDictionary<object, IList<string>> _subscriptionMapping;
	private IDictionary<string, List<Task>> _mailbox;
	private List<Task> _potentialTasksToDelete;
    
	protected Mailbox() {
		_subscriptionMapping = new Dictionary<object, IList<string>>();
		_mailbox = new Dictionary<string, List<Task>>();
		_potentialTasksToDelete = new List<Task>();
	}
    
	public void SubscribeToTaskType(object o, string taskType) {
		IList<string> existingKey = null;
		
		if (!_subscriptionMapping.TryGetValue(o, out existingKey)) {
			existingKey = _subscriptionMapping[o] = new List<string>();
		}
		existingKey.Add(taskType);
	}
	
	public void SendTask(Task task) {
		List<Task> tasks = null;
        
		if (!_mailbox.TryGetValue(task.GetTaskType(), out tasks)) {
			tasks = _mailbox[task.GetTaskType()] = new List<Task>();
		}
		tasks.Add(task);
	}
    
	public IList<Task> GetSubscribedTasksForType(object o, string msgType) {
		IList<string> msgTypes = null;
		List<Task> tasks = null;
        
		if (_subscriptionMapping.TryGetValue(o, out msgTypes)) {
			if (msgTypes.Contains(msgType)) {
				_mailbox.TryGetValue(msgType, out tasks);
			}
		}
        
        /*
  if (tasks != null) {
   _potentialTasksToDelete.AddRange(tasks);
  }
        */
		return tasks;
	}
    
    public void RemoveTask(Task msg) {
		// Remove from mailbox
		_mailbox[msg.GetTaskType()].RemoveAll(m => m != null && m.GetId() == msg.GetId());
		
		// Remove from potential mesages to delete
		_potentialTasksToDelete.RemoveAll(m => m != null &&  m.GetId() == msg.GetId());
	}
	
	public void Update() {
		IList<Task> msgsToDelete = new List<Task>();
        
		for (int i = 0; i < _potentialTasksToDelete.Count; i++) {
			Task msg = _potentialTasksToDelete[i];
			if (msg != null && msg.IsFinished()) {
				msgsToDelete.Add(msg);
			}
		}
        
		for (int i = 0; i < msgsToDelete.Count; i++) {
			Task msg = msgsToDelete[i];
			RemoveTask(msg);
		}
	}
    
}
