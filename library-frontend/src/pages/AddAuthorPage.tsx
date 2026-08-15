import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { createAuthor } from '../api/authorApi'

function AddAuthorPage() {

    const [fullName, setFullName] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()


    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')

        try {
            await createAuthor({ fullName })
            navigate('/add-book')
        } catch (err) {
            setError('Yazar eklenirken bir hata oluştu')
        }

    }




    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Yazar Ekle</h1>
                </div>
            </div>
            <div className="form-card">
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Yazar Adı</label>
                        <input value={fullName} onChange={(e) => setFullName(e.target.value)} />
                    </div>
                    {error && <div className="error-message">{error}</div>}
                    <button className="btn btn-primary" type="submit">Kaydet</button>
                </form>
            </div>
        </div>
    )
}

export default AddAuthorPage