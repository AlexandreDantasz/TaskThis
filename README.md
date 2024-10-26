# TaskThis

![GitHub repo size](https://img.shields.io/github/repo-size/AlexandreDantasz/TaskThis?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/AlexandreDantasz/TaskThis?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/AlexandreDantasz/TaskThis?style=for-the-badge)

<img src="imagem.png" alt="Exemplo imagem">

> Linha adicional de texto informativo sobre o que o projeto faz. Sua introdução deve ter cerca de 2 ou 3 linhas. Não exagere, as pessoas não vão ler.

### Ajustes e melhorias

O projeto ainda está em desenvolvimento e as próximas atualizações serão voltadas para as seguintes tarefas:

- [] Criar funcionalidade de registro de log de erros.
- [] Criar funcionalidade de manipulação da lista de tarefas.

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:

- Você instalou a versão 8.0.4 (ou superior) do <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0">.NET</a>.
- Você instalou e configurou corretamente o <a href="https://git-scm.com/downloads">Git</a> na sua máquina.
- Você tem uma máquina Windows, Linux ou Mac.
- Você leu e executou os passos definidos em [STARTUP](STARTUP.md).

## 🚀 Instalando TaskThis

Para instalar o TaskThis, siga estas etapas:

Para Linux, MacOs e Windows:

### Para instalar as dependências usadas:
```csharp
dotnet restore
```
### Para fazer a build da ferramente:
```csharp
dotnet build
```

### Para empacotar a ferramenta:
```csharp
dotnet pack
```
### Para instalar a ferramenta globalmente:
```csharp
dotnet tool install --global --add-source ./nupkg TaskThis 
```

## ☕ Usando TaskThis

### Segue um exemplo básico de uso do TaskThis:

```bash
TaskThis -goal "Como criar um Windows Forms em C#?"
```

OBS: a flag "-goal" é necessária para o funcionamento do TaskThis.

### Setando valores no timer do pomodoro:

A linha a seguir estabelece que o timer do pomodoro será composto por 10 minutos de foco no trabalho e 5 minutos de descanso.

```bash
TaskThis -goal "Como criar um Windows Forms em C#?" -work 10 -rest 5
```

### Para mais informações e ajuda:
```bash
TaskThis -h
```

## 📫 Contribuindo para <nome_do_projeto>

Para contribuir com <nome_do_projeto>, siga estas etapas:

1. Bifurque este repositório.
2. Crie um branch: `git checkout -b <nome_branch>`.
3. Faça suas alterações e confirme-as: `git commit -m '<mensagem_commit>'`
4. Envie para o branch original: `git push origin <nome_branch>`
5. Crie a solicitação de pull.

Como alternativa, consulte a documentação do GitHub em [como criar uma solicitação pull](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).


## 😄 Seja um dos contribuidores'

Quer fazer parte desse projeto? Clique [AQUI](CONTRIBUTING.md) e leia como contribuir.

## 📝 Licença

Esse projeto está sob licença. Veja o arquivo [LICENÇA](LICENSE) para mais detalhes.
