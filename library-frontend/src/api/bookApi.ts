import axiosClient from "./axiosClient";
import type { Book } from './types'
import type { CreateBookRequest } from "./types";


export async function getAllBooks(): Promise<Book[]> {
    const response = await axiosClient.get<Book[]>('/Books')
    return response.data
}

export async function createBook(data: CreateBookRequest): Promise<void> {
    await axiosClient.post('/Books', data)

}

export async function deleteBook(id: number): Promise<void> {
    await axiosClient.delete(`/Books/${id}`)

}