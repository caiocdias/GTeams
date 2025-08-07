package Models

type Colaborador struct {
	ID    uint   `json:"id" gorm:"primaryKey"`
	Nome  string `json:"nome" gorm:"size:100;not null" validate:"required,min=3,max=100"`
	User  string `json:"user" gorm:"size:50;not null;unique" validate:"required,min=3,max=50"`
	Cpf   string `json:"cpf" gorm:"size:14;not null;unique" validate:"required,len=14"`
	Ativo bool   `json:"ativo" gorm:"default:true"`
}
