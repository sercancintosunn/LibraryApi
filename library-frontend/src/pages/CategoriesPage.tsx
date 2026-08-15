import { useEffect, useState } from 'react'
import type { Category } from '../api/types'
import { useAuth } from '../context/AuthContext'
import { deleteCategory, getAllCategories } from '../api/categoryApi'
import { Link } from 'react-router-dom'

function CategoriesPage() {
    const [categories, setCategories] = useState<Category[]>([])
    const { currentUser } = useAuth()
    const isAdmin = currentUser?.role === 'Admin'

    useEffect(() => {
        loadCategories()
    }, [])

    function loadCategories() {
        getAllCategories().then(setCategories)
    }

    async function handleDelete(id: number) {
        if (!confirm("Bu kategoriyi silmek istediğinize emin misiniz?")) return

        try {
            await deleteCategory(id)
            loadCategories()
        } catch (err: any) {
            const message = err.response?.data?.message || "Kategori silinirken bir hata oluştu"
            alert(message)
        }
    }




    return (
        <div className='container'>
            <h1>Kategoriler</h1>
            <Link to="/add-category">+ Yeni Kategori Ekle</Link>
            <ul>
                {categories.map((category) => (
                    <li key={category.id}>
                        {category.name}{' '}
                        {isAdmin && (
                            <button onClick={() => handleDelete(category.id)}>Sil</button>
                        )}
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default CategoriesPage