import React, { useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { createBook } from '../api/bookApi'
import { getAllAuthors } from '../api/authorApi'
import { getAllCategories } from '../api/categoryApi'
import type { Author, Category } from '../api/types'



function AddBookPage() {

    const [title, setTitle] = useState('')
    const [isbn, setIsbn] = useState('')
    const [authorId, setAuthorId] = useState('')
    const [categoryId, setCategoryId] = useState('')

    const [authors, setAuthors] = useState<Author[]>([])
    const [categories, setCategories] = useState<Category[]>([])

    const [error, setError] = useState('')
    const navigate = useNavigate()

    useEffect(() => {
        getAllAuthors().then(setAuthors)
        getAllCategories().then(setCategories)
    }, [])

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')

        try {
            await createBook({
                title,
                isbn: isbn || null,
                authorId: Number(authorId),
                categoryId: Number(categoryId),
            })
            navigate('/')
        } catch (err: any) {
            console.error(err.response?.data || err.message)
            setError('Kitap eklenirken bir hata oluştu')
        }
    }

    return (
        <div className='container'>
            <h1>Kitap Ekle</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="">Kitap Adı</label>
                    <input value={title} onChange={(e) => setTitle(e.target.value)} />
                </div>

                <div>
                    <label htmlFor="">ISBN (opsiyonel)</label>
                    <input value={isbn} onChange={(e) => setIsbn(e.target.value)} />
                </div>

                <div>
                    <label htmlFor="">Yazar</label>
                    <select value={authorId} onChange={(e) => setAuthorId(e.target.value)}>
                        <option value="">-- Seçiniz --</option>
                        {authors.map((author) => (
                            <option key={author.id} value={author.id}>{author.fullName}</option>
                        ))}
                    </select>
                    <Link to='/add-author'>+ Yeni yazar Ekle</Link>
                </div>

                <div>
                    <label htmlFor="">Kategori</label>
                    <select value={categoryId} onChange={(e) => setCategoryId(e.target.value)}>
                        <option value="">-- Seçiniz --</option>
                        {categories.map((category) => (
                            <option key={category.id} value={category.id}>{category.name}</option>
                        ))}
                    </select>
                    <Link to='/add-category'>+ Yeni kategori ekle</Link>
                </div>

                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type='submit'>Kaydet</button>


            </form>
        </div>
    )
}

export default AddBookPage