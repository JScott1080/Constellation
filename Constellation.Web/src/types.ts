export interface Company {
    id: string
    name: string
    slug: string
}

export interface Project {
    id: string
    name: string
    description: string | null
    statusId: string
    companyId: string
}

export interface BoardColumn {
    tasks: any
    id: string
    name: string
    order: number
}

export interface Board {
    id: string
    name: string
    projectId: string
    columns: BoardColumn[]
}

export interface TaskItem{
    id: string
    title: string
    description: string | null
    order: number
    columnId: string
}

export interface TaskAssignment {
    id: string
    taskId: string
    isLead: boolean
}

export interface TaskComment {
    id: string
    userId: string
    content: string
    fileRecordId: string | null
}