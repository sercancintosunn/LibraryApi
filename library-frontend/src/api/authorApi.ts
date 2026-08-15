import axiosClient from "./axiosClient";
import type { Author, CreateAuthorRequest } from "./types";


export async function getAllAuthors(): Promise<Author[]> {
    const response = await axiosClient.get<Author[]>('/Authors')
    return response.data
}

export async function createAuthor(data: CreateAuthorRequest): Promise<void> {
    await axiosClient.post('/Authors', data)

}

export async function deleteAuthor(id: number): Promise<void> {
    await axiosClient.delete(`/Authors/${id}`)
}