"use client";

import React, { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import ChatBubbleIcon from '@mui/icons-material/ChatBubbleOutline';
import CommentBar from "@/app/components/CommentBar/CommentBar";

//import Image from "next/image";

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

const PostPage = () => {
  const { id } = useParams(); // Access to the specific id for the post
  //const [product, setProduct] = useState<Product | null>(null); // Single product initialization. Is either null or an object
   const [post, setPost] = useState<Post | null>(null);
          const [comments, setComments] = useState<Post[]>([]);
 

  useEffect(() => {
    if (id) {
      const fetchPost = async () => {
        try {
          const response = await fetch(`http://localhost:5092/api/Posts/${id}`);
          if (!response.ok) {
            throw new Error("Post not found");
          }
          const data = await response.json();
          setPost(data);

        
        } catch (error) {
          console.error("Error fetching post:", error);
        }
      };
      fetchPost();
    }
  }, [id]);

  return post ? (
    <div className="flex justify-center items-center flex-col overflow-y-auto">
        <div className=" mt-60 ml-60 flex justify-center items-center text-left ">
            <div key={post.id} className="p-4 border-b">
                <h2 className="text-xl font-bold mb-10">{post.title}</h2>
                <p>{post.content}</p>
                <p className="text-sm text-gray-500">
                Publicerad: {new Date(post.publishedDate).toLocaleDateString()}
                </p>
                <p><ChatBubbleIcon/></p>
                <CommentBar/>
            </div>
          </div>
          <div className="">
            <h3 className="">Description</h3>
            <p className=""></p>
          </div>
      </div>
  ) : (
    <div>Loading...</div>
  );
};

export default PostPage;
