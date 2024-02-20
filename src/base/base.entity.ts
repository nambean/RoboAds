import { BaseEntity, Column, CreateDateColumn, UpdateDateColumn } from 'typeorm';

export abstract class CustomBaseEntity extends BaseEntity {
    @CreateDateColumn({ name: 'created_at', nullable: true })
    public createdAt: Date;

    @Column({ name: 'created_by', nullable: true })
    public createdBy: number;

    @UpdateDateColumn({ name: 'update_at', nullable: true })
    public updateAt: Date;

    @Column({ name: 'update_by', nullable: true })
    public updateBy: number;
}
