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
        <div className='container'>
            <h1>Yazar Ekle</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label >Yazar Adı</label>
                    <input value={fullName} onChange={(e) => setFullName(e.target.value)} />
                </div>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type='submit'>Kaydet</button>
            </form>
        </div>
    )
}

export default AddAuthorPage