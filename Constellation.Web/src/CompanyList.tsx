import { useState, useEffect } from 'react'
import type {Company} from './types'

interface CompanyListProps {
    onSelect: (companyId: string) => void
}

function CompanyList({ onSelect }: CompanyListProps) {
    const [companies, setCompanies] = useState<Company[]>([])

    useEffect(() => {
        async function fetchCompanies() {
            const response = await fetch('http://localhost:5160/api/companies')
            const data = await response.json()
            setCompanies(data)
        }
        fetchCompanies()
    }, [])

    return (
        <div>
            <h2>Companies</h2>
            <ul>
                {companies.map((company) => (
                    <li key={company.id} onClick={() => onSelect(company.id)}>
                        {company.name}
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default CompanyList