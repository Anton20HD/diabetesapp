
"use client";
import React, {useEffect, useState} from 'react';

interface Comment {
    id: number;
    content: string;
    userId: number;
}

interface Post {
    id: number;
    title: string;
    content: string;
    publishedDate: string;
    userId: number;
    comments: Comment[];
}


const PostPage = () => {
        const [posts, setPosts] = useState<Post[]>([]);

        useEffect(() => {
            const fetchPosts = async () => {
                const res = await fetch("http://localhost:5092/api/Posts");
                const data = await res.json();
                setPosts(data);
            }
            fetchPosts();
        }, []);



  return (
    <div className="">
        {posts.map((post) => (
            <div key={post.id} className="p-4 border-b">
                <h2 className="text-xl font-bold">{post.title}</h2>
                <p>{post.content}</p>
                <p className="text-sm text-gray-500">
                Publicerad: {new Date(post.publishedDate).toLocaleDateString()}
                </p>
            </div>
        ))}
    </div>
  )
}

export default PostPage