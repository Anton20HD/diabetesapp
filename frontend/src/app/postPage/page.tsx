"use client";
import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";

interface Comment {
  id: number;
  author: string;
  content: string;
  publishedDate: string;
  userId: number;
  postId: number;
}

interface Post {
  id: number;
  title: string;
  content: string;
  publishedDate: string;
  userId: number;
  comments: Comment[];
}

const AllPostsPage = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [comments, setComments] = useState<Post[]>([]);
  const [activePostId, setActivePostId] = useState<number | null>(null);
  const router = useRouter();

  useEffect(() => {
    const fetchPosts = async () => {
      const res = await fetch("http://localhost:5092/api/Posts");
      const data = await res.json();
      setPosts(data);
    };
    fetchPosts();
  }, []);

  useEffect(() => {
    const fetchComments = async () => {
      const res = await fetch("http://localhost:5092/api/Comments/1");
      const data = await res.json();
      setComments(data);
    };
    fetchComments();
  }, []);

  const handlePost = (postId: Number) => {
    router.push(`/postPage/${postId}`);
  };

  return (
    <div className="">
      {posts.map((post) => (
        <div key={post.id} onClick={() => handlePost(post.id)} className="p-4 border-b cursor-pointer">
          <h2 className="text-xl font-bold">{post.title}</h2>
          <p>{post.content}</p>
          <p className="text-sm text-gray-500">
            Publicerad: {new Date(post.publishedDate).toLocaleDateString()}
          </p>
        </div>
      ))}
    </div>
  );
};

export default AllPostsPage;
