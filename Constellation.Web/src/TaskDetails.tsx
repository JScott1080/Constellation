import { useState, useEffect } from 'react'
import type { TaskItem, TaskAssignment, TaskComment } from './types'

interface TaskDetailsProps {
    task: TaskItem
    onBack: () => void
}

function TaskDetail({ task, onBack}: TaskDetailsProps) {
    const [assignments, setAssignments] = useState<TaskAssignment[]>([])
    const [comments, setComments] = useState<TaskComment[]>([])

    useEffect(() => {
      async function fetchAssignments() {
        const response = await fetch(`http://localhost:5160/api/tasks/${task.id}/assignments`)
        const data = await response.json()
        setAssignments(data)
      }
      fetchAssignments()
    }, [task.id])

    useEffect(() => {
      async function fetchComments(){
        const responce = await fetch('http://localhost:5160/api/tasks/${task.id}/comments')
        const data = await responce.json()
        setComments(data)
      }
      fetchComments()
    }, [task.id])

    return (
    <div>
      <button onClick={onBack}>← Back to board</button>
      <h2>{task.title}</h2>
      <p>{task.description ?? 'No description'}</p>

      <h3>Assignments</h3>
      {/* TODO: render assignments — no Users UI exists yet, so just show
          the raw userId and whether isLead for each, e.g. as a <ul> */
          }

      <h3>Comments</h3>
      {/* TODO: render comments — content plus userId, same idea */}
    </div>
  )
}

export default TaskDetail