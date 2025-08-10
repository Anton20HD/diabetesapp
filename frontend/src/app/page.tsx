import Image from "next/image";
import StartPage from "./startPage/page";

export default function Home() {
  return (
    <>
      <main className="w-screen bg-white scroll-smooth bg-cover bg-no-repeat overflow-x-hidden m-0 p-0">
          <StartPage/>
      </main>
    </>
  );
}

