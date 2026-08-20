import { useState, useEffect } from 'react'
import type { BoardColumn, TaskItem } from './types'

interface ColumnProps {
  column: BoardColumn
  onSelectTask?: (taskId: string) => void
}

function Column({ column, onSelectTask }: ColumnProps) {
  const [tasks, setTasks] = useState<TaskItem[]>([])
  const [newTitle, setNewTitle] = useState('')

  useEffect(() => {
    async function fetchTasks() {
      const response = await fetch(`http://localhost:5160/api/columns/${column.id}/tasks`)
      const data = await response.json()
      setTasks(data)
    }
    fetchTasks()
  }, [column.id])

  async function handleAddTask(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault()
    if (newTitle.trim() === '') return
    const response = await fetch(`http://localhost:5160/api/columns/${column.id}/tasks`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ title: newTitle }),
    })
    if (response.ok) {
      const newTask = await response.json()
      setTasks((prevTasks) => [...prevTasks, newTask])
      setNewTitle('')
    }
  }

  async function handleSelectTask(taskId: string) {
    if (onSelectTask) {
      onSelectTask(taskId)
    }
  }

  return (
    <div className="column">
      <h3>{column.name}</h3>
      <ul>
        {tasks.map((task) => (
          <li key={task.id} onClick={() => handleSelectTask(task.id)}>
            {task.title}
          </li>
        ))}
      </ul>
      <form onSubmit={handleAddTask}>
        <input
          value={newTitle}
          onChange={(e) => setNewTitle(e.target.value)}
          placeholder="New task title"
        />
        <button type="submit">Add</button>
      </form>
    </div>
  )
}

export default Column