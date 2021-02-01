import { Lending } from './Lending.model'

export class productToGmh {
    public Name: string
    public ProductCodeToGMH: number
    public ProductCode: number
    public GmhCode: number
    public Images: string[]
    public Amount: number
    public FreeDescription: string;
    public IsDisposable: boolean
    public SecurityDepositAmount: number
    public Status: string
    public Lendings: Lending[]
}
export class Images{
    public ImageCode :number
    public ProductCodeToGMH :number
    public Path:string
}