export interface EditBlogPostRequest {
    title: string;
    shortDescription: string;
    content: string;
    featureImage: string;
    urlHandle: string;
    author: string;
    publishedDate: Date;
    isVisible: boolean;
    categories: string[];
}