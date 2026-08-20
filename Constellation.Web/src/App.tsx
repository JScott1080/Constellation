import { useState, useEffect } from 'react'
import './App.css'

import { Board } from './types'
import Column from './Column'
import ProjectList from './ProjectList'
import CompanyList from './CompanyList'

function App() {
    const [selectedCompany, setSelectedCompany] = useState<string | null>(null)
    const [selectedProject, setSelectedProject] = useState<string | null>(null)
    const [boards, setBoards] = useState<Board[]>([])
    const [selectedTaskId, setSelectedTaskId] = useState<string | null>(null)

    useEffect(() => {
        if (selectedProject) {
            async function fetchBoards() {
                const response = await fetch(`http://localhost:5160/api/projects/${selectedProject}/boards`)
                const data = await response.json()
                setBoards(data)
            }
            fetchBoards()
        }
    }, [selectedProject])
    

    return (
        <div className="App">
            <h1>Constellation</h1>
            {!selectedCompany ? (
                <div>
                    <h2>Companies</h2>
                    <ul>
                        <CompanyList onSelect={(companyId) => setSelectedCompany(companyId)} />
                    </ul>
                </div>
            ) : !selectedProject ? (
                <div>
                    <button onClick={() => setSelectedCompany(null)}>← Back to companies</button>
                    <h2>Projects</h2>
                    <ul>
                        <ProjectList
                            id={selectedCompany}
                            onSelect={(projectId) => setSelectedProject(projectId)}
                            onBack={() => setSelectedCompany(null)}
                        /> 
                    </ul>
                </div>
            ) : (
                <div>
                    <button onClick={() => setSelectedProject(null)}>← Back to projects</button>
                    <h2>Boards</h2>
                    {boards.map((board) => (
                        <div key={board.id} className="board">
                            <h3>{board.name}</h3>
                            <div className="columns">
                                {board.columns.map((column) => (
                                    <Column key={column.id} column={column} onSelectTask={setSelectedTaskId} />
                                ))}
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
  )
}

export default App