import React, { useEffect, useState} from 'react'; 
import { Link } from 'react-router-dom';
import { getTasks, deleteTask } from '../data/loader';
import '../App.css';

export default function TaskList() {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    async function loadTasks() {
      const data = await getTasks();
      setTasks(data);
    }
    loadTasks();
  }, [])

  const handleDelete = async (id) => {
    await deleteTask(id);
    setTasks(tasks.filter((t) => t.id !== id));
  };

  return (
    <div className="App">
      <h1>Tasks</h1>
      <Link to="/add">
        <button  className="add">Add Task</button>
      </Link>
      <table>
        <thead>
          <tr>
            <th>Case Id</th>
            <th>Title</th>
            <th>Description</th>
            <th>Status</th>
            <th>Due Date</th>
          </tr>
        </thead>
        <tbody>
          {(tasks ?? []).map((task) => (
            <tr key={task.id}>
              <td>{task.case_id}</td>
              <td>{task.title}</td>
              <td>{task.description}</td>
              <td>{task.status}</td>
              <td>{new Date(task.due).toLocaleString("en-GB")}</td>
              <Link to={`/edit/${+ task.id}`}>
                <td><button className="update">Update</button></td>
              </Link>
              <td><button onClick={() => handleDelete(task.id)} className="delete">Delete</button></td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
