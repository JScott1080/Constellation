import { useState, useEffect } from 'react'
import type { Project } from './types'

interface ProjectListProps {
    id: string
    onSelect: (projectId: string) => void
    onBack: () => void
}

function ProjectList({ id, onSelect, onBack }: ProjectListProps) {
    const [projects, setProjects] = useState<Project[]>([])

    useEffect(() => {
        async function fetchProjects() {
            const response = await fetch(`http://localhost:5160/api/companies/${id}/projects`)
            const data = await response.json()
            setProjects(data)
        }
        fetchProjects()
    }, [id ])

    return (
    <div>
      <button onClick={onBack}>← Back to companies</button>
      <h2>Projects</h2>
      {projects.length === 0 ? (
        <p>No projects found for this company.</p>
      ) : (
        <ul>
          {projects.map((project) => (
            <li key={project.id} onClick={() => onSelect(project.id)}>
              {project.name}
            </li>
          ))}
        </ul>
      )}
    </div>
    )
}

export default ProjectList