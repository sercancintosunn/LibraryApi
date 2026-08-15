import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { createCategory } from '../api/categoryApi'

function AddCategoryPage() {

    const [name, setName] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')

        try {
            await createCategory({ name })
            navigate('/add-book')
        } catch (err) {
            setError('Kategori eklenirken bir hata oluştu')
        }

    }
    return (
        <div className='container'>
            <h1>Kategori Ekle</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="">Kategori Adı</label>
                    <input value={name} onChange={(e) => setName(e.target.value)} />
                </div>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type='submit'>Kaydet</button>
            </form>
        </div>
    )
}

export default AddCategoryPage