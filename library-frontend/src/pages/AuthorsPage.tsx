import { useEffect, useState } from 'react'
import { useAuth } from '../context/AuthContext'
import { deleteAuthor, getAllAuthors } from '../api/authorApi'
import { Link } from 'react-router-dom'
import type { Author } from '../api/types'

function AuthorsPage() {

    const [authors, setAuthors] = useState<Author[]>([])
    const { currentUser } = useAuth()
    const isAdmin = currentUser?.role === 'Admin'

    useEffect(() => {
        loadAuthors()
    }, [])

    function loadAuthors() {
        getAllAuthors().then(setAuthors)
    }

    async function handleDelete(id: number) {
        if (!confirm("Bu yazarı silmek istediğinize emin misiniz?")) return

        try {
            await deleteAuthor(id)
            loadAuthors()
        } catch (err: any) {
            const message = err.response?.data?.message || 'yazar silinirken bir hata oluştu.'
            alert(message)
        }
    }
    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Yazarlar</h1>
                </div>
                <Link className="btn btn-primary" to="/add-author">+ Yeni Yazar</Link>
            </div>

            <div className="data-table-wrapper">
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>Ad Soyad</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {authors.map((author) => (
                            <tr key={author.id}>
                                <td>{author.fullName}</td>
                                <td>
                                    {isAdmin && (
                                        <button className="btn btn-danger" onClick={() => handleDelete(author.id)}>
                                            Sil
                                        </button>
                                    )}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}

export default AuthorsPage