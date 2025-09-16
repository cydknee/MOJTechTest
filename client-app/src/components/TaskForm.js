import React, { useEffect, useState} from 'react'; 
import { createTask, getTask, updateTask } from '../data/loader';
import { useNavigate, useParams } from 'react-router-dom';

export default function TaskForm() {
    const [case_id, setCaseId] = useState('');
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [status, setStatus] = useState('');
    const [due, setDue] = useState('');

    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            async function loadTask() {
                const task = await getTask(id);
                setCaseId(task.case_id);
                setTitle(task.title);
                setDescription(task.description);
                setStatus(task.status);
                setDue(task.due);
            }
            loadTask();
        }
    }, [id]);

    const handleSubmit = async () => { 
        const taskData = { 
            case_id, 
            title, 
            description,
            status, 
            due: new Date(due).toISOString() 
        }; 

        if (id) {
            await updateTask(id, taskData);
        } else {
            await createTask(taskData);
        }


        navigate("/");
    };

    return(
        <div>
            <h1>{id ? "Edit Task" : "Add Task"}</h1>
            <div>
                <label>Case Id</label>
                <input value={case_id} onChange={e => setCaseId(e.target.value)} />
            </div>
            <div>
                <label>Title</label>
                <input value={title} onChange={e => setTitle(e.target.value)} />
            </div>
            <div>
                <label>Description</label>
                <input value={description} onChange={e => setDescription(e.target.value)} />
            </div>
            <div>
                <label>Status</label>
                <input value={status} onChange={e => setStatus(e.target.value)} />
            </div>
            <div>
                <label>Due Date</label>
                <input value={due} onChange={e => setDue(e.target.value)} />
            </div>
            <button className='add' onClick={handleSubmit}>{id ? "Update Task" : "Add Task"}</button>
        </div>
    );
}