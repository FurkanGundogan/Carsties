'use client'
import { useParamsStore } from '@/hooks/useParamsStore'
import { usePathname, useRouter } from 'next/navigation'
import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'

export default function Logo() {
    const router = useRouter()
    const pathname = usePathname();
    const reset = useParamsStore(state => state.reset)
    function doReset(){
        if(pathname !== '/') router.push('/')
        reset();
    }
    return (
        <div onClick={doReset} className='cursor-pointer flex items-center gap-2 text-3xl font-bold text-red-700'>
            <AiOutlineCar size={36} />
            <div>Carsties Auctions</div>
        </div>
    )
}
