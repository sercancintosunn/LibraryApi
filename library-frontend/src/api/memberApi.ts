import axiosClient from "./axiosClient";
import type { Member } from "./types";

export async function getMe(): Promise<Member> {
    const response = await axiosClient.get<Member>('/Members/me')
    return response.data
}