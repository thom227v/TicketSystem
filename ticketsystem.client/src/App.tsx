import { useEffect, useState } from 'react';
import './App.css';
import SignUpForm from './components/SignUpForm'
interface Department {
    id: number;
    name: string;
}

function App() {
    const [departments, setDepartments] = useState<Department[]>();
    const [departmentName, setDepartmentName] = useState<string>('');

    useEffect(() => {
        getDepartments();
    }, []);

  const handleSignUpSuccess = () => {
    console.log("SIGN UP WAS SUCCESS")
  }

    const contents = departments === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {departments.map(department =>
                    <tr key={department.id}>
                        <td>{department.id}</td>
                        <td>{department.name}</td>
                    </tr>
                )}
            </tbody>
        </table>;



    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        <div>
            <textarea placeholder="Enter department name..." onChange={e => setDepartmentName(e.target.value)}></textarea>
            <button onClick={addDepartment}>Add Department</button>
        </div>
        <SignUpForm onSuccess={handleSignUpSuccess} />
        </div>
    );

    async function getDepartments() {
        const response = await fetch('/ticket/GetDepartments');
        if (response.ok) {
            const data = await response.json();
            console.log("data", data);
            setDepartments(data);
        }
    };

    async function addDepartment() {
        if (departmentName) {
            const response = await fetch('/ticket/CreateDepartment', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(departmentName)
            });
            if (response.ok) {
                getDepartments();
                setDepartmentName('');
            }
        }

 }
}

export default App;