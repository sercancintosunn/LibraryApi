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
        <div className='container'>
            <h1>Yazarlar</h1>
            <Link to='/add-authors'>+ Yeni Yazar Ekle</Link>
            <ul>
                {authors.map((author) => (
                    <li key={author.id}>
                        {author.fullName}{' '}
                        {isAdmin && (
                            <button onClick={() => handleDelete(author.id)}>Sil</button>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default AuthorsPage