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
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Kategoriler</h1>
                </div>
                <Link className="btn btn-primary" to="/add-category">+ Yeni Kategori</Link>
            </div>

            <div className="data-table-wrapper">
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>Ad</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {categories.map((category) => (
                            <tr key={category.id}>
                                <td>{category.name}</td>
                                <td>
                                    {isAdmin && (
                                        <button className="btn btn-danger" onClick={() => handleDelete(category.id)}>
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

export default CategoriesPage