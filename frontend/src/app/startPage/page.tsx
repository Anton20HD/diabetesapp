"use client";

import React from "react";

//import Products from "@/app/components/products/page";


const StartPage = () => {
  // const [products, setProducts] = useState([]);
  // useEffect(() => {
  //   fetch("")
  //     .then((res) => {
  //       return res.json();
  //     })
  //     .then((data) => {
  //       console.log(data);
  //       setProducts(data);
  //     });
  // }, []);






  return (
    <div className="h-screen bg-repeat bg-cover bg-center absolute overflow-hidden">
      <div>
        <video
          className="w-600 h-150  object-cover"
          src="/videos/startpage.mp4"
          autoPlay
          loop
          muted
        />
      </div>

      

      {/* <Products /> */}
      
    </div>
  );
};

export default StartPage;
