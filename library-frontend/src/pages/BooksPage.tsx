import { useEffect, useState } from 'react'
import { deleteBook, getAllBooks } from '../api/bookApi'
import type { Book } from '../api/types'
import { useAuth } from '../context/AuthContext'
import { createLoan } from '../api/loanApi'

function BooksPage() {

    const [books, setBooks] = useState<Book[]>([])
    const [message, setMessage] = useState('')
    const { currentUser } = useAuth()

    useEffect(() => {
        loadBooks()
    }, [])

    function loadBooks() {
        getAllBooks().then(setBooks)
    }

    async function handeBorrow(bookId: number) {
        if (!currentUser) return

        setMessage('')
        try {
            await createLoan({ bookId, memberId: currentUser.id })
            setMessage('Kitap başarıyla ödünç alındı')
        } catch (err) {
            setMessage("Ödünç alma başarısız.Muhtemelen iade edilnmemiş bir kitabınız var")
        }

    }


    async function handleDelete(bookId: number) {
        if (!confirm('Bu kitabı silmek istediğinize emin misiniz?')) return

        try {
            await deleteBook(bookId)
            loadBooks()
        } catch (err) {
            alert("Kitap silinirken bir hata oluştu")
        }
    }

    const isAdmin = currentUser?.role === 'Admin'


    return (
        <div className='container'>
            <h1>Kütüphane Sistemi</h1>
            {message && <p>{message}</p>}
            <ul>
                {books.map((book) => (
                    <li key={book.id}>
                        {book.title} - {book.authorName}
                        <button onClick={() => handeBorrow(book.id)}>Ödünç Al</button>
                        {isAdmin && (
                            <button onClick={() => handleDelete(book.id)}>Sil</button>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default BooksPage