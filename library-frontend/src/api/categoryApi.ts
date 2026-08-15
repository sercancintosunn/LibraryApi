
import axiosClient from "./axiosClient";
import type { Category, CreateCategoryRequest } from "./types";


export async function getAllCategories(): Promise<Category[]> {
    const response = await axiosClient.get<Category[]>('/Categories')
    return response.data
}

export async function createCategory(data: CreateCategoryRequest): Promise<void> {
    await axiosClient.post('/Categories', data)
}

export async function deleteCategory(id: number): Promise<void> {
    await axiosClient.delete(`/Categories/${id}`)

}