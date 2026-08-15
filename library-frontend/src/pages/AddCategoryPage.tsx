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
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Kategori Ekle</h1>
                </div>
            </div>
            <div className="form-card">
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Kategori Adı</label>
                        <input value={name} onChange={(e) => setName(e.target.value)} />
                    </div>
                    {error && <div className="error-message">{error}</div>}
                    <button className="btn btn-primary" type="submit">Kaydet</button>
                </form>
            </div>
        </div>
    )
}

export default AddCategoryPage